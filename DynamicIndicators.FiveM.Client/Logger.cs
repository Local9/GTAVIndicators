﻿using System;

namespace DynamicIndicators.FiveM.Client
{
    // TODO: Convert so both Scripthook and FiveM versions use one file.
    public class Log
    {
        // Colors
        public const string LIGHT_RED = "^1";
        public const string LIGHT_GREEN = "^2";
        public const string YELLOW = "^3";
        public const string DARK_BLUE = "^4";
        public const string LIGHT_BLUE = "^5";
        public const string PURPLE = "^6";
        public const string WHITE = "^7";
        public const string DARK_RED = "^8";
        public const string PINK = "^9";

        public Log() { }

        /// <summary>
        /// Sends an green info message in console
        /// </summary>
        /// <param name="text">Text of the message</param>
        public void Info(string text)
        {
            string timestamp = $"{DateTime.Now:dd/MM/yyyy, HH:mm}";
            string errorPrefix = "-- [INFO] -- ";
            string color = LIGHT_GREEN;
            CitizenFX.Core.Debug.WriteLine($"{color}{timestamp} {errorPrefix} {text}.^7");
        }

        /// <summary>
        /// Sends a purple debug message in console. (it checks for 'fxevents_debug_mode' in the fxmanifest)
        /// </summary>
        /// <param name="text">Text of the message</param>
        public void Debug(string text)
        {
            string timestamp = $"{DateTime.Now:dd/MM/yyyy, HH:mm}";
            string errorPrefix = "-- [DEBUG] -- ";
            string color = LIGHT_BLUE;
            CitizenFX.Core.Debug.WriteLine($"{color}{timestamp} {errorPrefix} {text}.^7");
        }

        /// <summary>
        /// Sends a yellow Warning message
        /// </summary>
        /// <param name="text">Text of the message</param>
        public void Warning(string text)
        {
            string timestamp = $"{DateTime.Now:dd/MM/yyyy, HH:mm}";
            string errorPrefix = "-- [WARNING] --";
            string color = YELLOW;
            CitizenFX.Core.Debug.WriteLine($"{color}{timestamp} {errorPrefix} {text}.^7");
        }

        /// <summary>
        /// Sends a red Error message
        /// </summary>
        /// <param name="text">Text of the message</param>
        public void Error(string text)
        {
            string timestamp = $"{DateTime.Now:dd/MM/yyyy, HH:mm}";
            string errorPrefix = "-- [ERROR] -- ";
            string color = LIGHT_RED;
            CitizenFX.Core.Debug.WriteLine($"{color}{timestamp} {errorPrefix} {text}.^7");
        }

        /// <summary>
        /// Sends a dark red Fatal Error message
        /// </summary>
        /// <param name="text">Text of the message</param>
        public void Fatal(string text)
        {
            string timestamp = $"{DateTime.Now:dd/MM/yyyy, HH:mm}";
            string errorPrefix = "-- [FATAL] -- ";
            string color = DARK_RED;
            CitizenFX.Core.Debug.WriteLine($"{color}{timestamp} {errorPrefix} {text}.^7");
        }
    }
}
