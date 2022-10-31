using CitizenFX.Core.Native;
using DynamicIndicators.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicIndicators.FiveM.Client
{
    public class Main : BaseScript
    {
        private const string ANIM_INDICATOR_RIGHT_OFF = "indirightoff";
        private const string ANIM_INDICATOR_RIGHT_ON = "indirighton";
        private const string ANIM_INDICATOR_LEFT_OFF = "indileftoff";
        private const string ANIM_INDICATOR_LEFT_ON = "indilefton";
        private const string ANIM_INDICATOR_HAZARD_OFF = "indihazoff";
        private const string ANIM_INDICATOR_HAZARD_ON = "indihazon";
        
        public static Log Log = new();
        
        List<BlinkerParameters> _blinkerParameters = new();
        Vehicle _currentVehicle = null;
        BlinkerParameters _currentBlinkerParameters = null;

        bool _indicatorRightEnabled;
        bool _indicatorLeftEnabled;
        bool _indicatorHazardEnabled;

        bool _indicatorRightManaged;
        bool _indicatorLeftManaged;
        bool _indicatorHazardManaged;

        bool _timerEnabled;
        long _globalGameTimer;

        public Main()
        {
            _blinkerParameters = Configuration.GetConfig().Vehicles;
            EventHandlers["gameEventTriggered"] += new Action<string, List<dynamic>>(OnGameEventTriggered);
        }

        public void AttachTickHandler(Func<Task> task)
        {
            Tick += task;
        }

        public void DetachTickHandler(Func<Task> task)
        {
            Tick -= task;
        }

        private void OnGameEventTriggered(string eventName, List<dynamic> args)
        {
            if (eventName == "CEventNetworkPlayerEnteredVehicle")
            {
                Player player = new Player((int)args[0]);

                // if the player tiggering the event is not the current player, return
                if (player.ServerId != Game.Player.ServerId) return;

                int entityId = (int)args[1];
                if (!API.IsEntityAVehicle(entityId)) return;

                _currentVehicle = new Vehicle(entityId);

                // We only want the driver if they are the current player
                if (_currentVehicle.Driver != Game.PlayerPed) return;

                _currentBlinkerParameters = _blinkerParameters.FirstOrDefault(x => GetHashKey(x.ModelName) == _currentVehicle.Model.Hash);

                if (!_currentVehicle.Model.IsTrain && _currentBlinkerParameters is not null)
                {
                    RequestAnimDict($"va_{_currentBlinkerParameters.ModelName}");
                    
                    if (_currentBlinkerParameters.Duration == 0)
                    {
                        // soon
                    }
                    
                    if (_currentBlinkerParameters.Duration > 0)
                        AttachTickHandler(AnimateBlinkersAsync);
                }
            }
        }

        private async Task AnimateBlinkersAsync()
        {
            bool areHazardsEnabled = _currentVehicle.IsLeftIndicatorLightOn && _currentVehicle.IsRightIndicatorLightOn;
            bool isEngineRunning = _currentVehicle.IsEngineRunning;

            bool isPlayingAnimationHazadsOff = IsAnimationPlaying(ANIM_INDICATOR_HAZARD_OFF);
            bool isPlayingAnimationLeftOff = IsAnimationPlaying(ANIM_INDICATOR_LEFT_OFF);
            bool isPlayingAnimationRightOff = IsAnimationPlaying(ANIM_INDICATOR_RIGHT_OFF);

            bool isPlayingAnimationHazadsOn = IsAnimationPlaying(ANIM_INDICATOR_HAZARD_ON);
            bool isPlayingAnimationLeftOn = IsAnimationPlaying(ANIM_INDICATOR_LEFT_ON);
            bool isPlayingAnimationRightOn = IsAnimationPlaying(ANIM_INDICATOR_RIGHT_ON);

            if (isEngineRunning)
            {
                if (areHazardsEnabled)
                {
                    // Enable Hazard Lights
                    if (_indicatorRightEnabled)
                    {
                        if (!isPlayingAnimationRightOff)
                            PlayAnimation(ANIM_INDICATOR_RIGHT_OFF);
                        else
                        {
                            _indicatorRightEnabled = false;
                            _timerEnabled = false;
                        }
                    }
                    else if (_indicatorLeftEnabled)
                    {
                        if (!isPlayingAnimationLeftOff)
                            PlayAnimation(ANIM_INDICATOR_LEFT_OFF);
                        else
                        {
                            _indicatorLeftEnabled = false;
                            _timerEnabled = false;
                        }
                    }
                    else
                    {
                        if (!_timerEnabled)
                        {
                            _globalGameTimer = GetGameTimer();
                            _timerEnabled = true;

                            if (!_indicatorHazardManaged)
                            {
                                PlayAnimation(ANIM_INDICATOR_HAZARD_ON);
                                _indicatorHazardManaged = true;
                                _timerEnabled = false;
                            }
                        }

                        if (_globalGameTimer - GetGameTimer() > _currentBlinkerParameters.Duration)
                        {
                            PlayAnimation(ANIM_INDICATOR_HAZARD_ON);
                            _timerEnabled = false;
                        }

                        _indicatorHazardEnabled = true;
                        _indicatorRightManaged = false;
                        _indicatorLeftManaged = false;
                    }
                }
                else if (_currentVehicle.IsLeftIndicatorLightOn)
                {
                    
                }
                else if (_currentVehicle.IsRightIndicatorLightOn)
                {
                    
                }
                else
                {
                    
                }
            }
            else
            {
                // turn off all
            }
        }

        bool IsAnimationPlaying(string animationName)
        {
            return IsEntityPlayingAnim(_currentVehicle.Handle, $"va_{_currentBlinkerParameters.ModelName}", animationName, 3);
        }

        void PlayAnimation(string animationName)
        {
            PlayEntityAnim(_currentVehicle.Handle, animationName, $"va_{_currentBlinkerParameters.ModelName}", 8.0f, false, false, false, 0, 0);
        }
    }
}
