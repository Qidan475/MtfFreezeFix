﻿using Exiled.API.Interfaces;

namespace MtfFreezeFix
{
    public class PluginConfig: IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;
    }
}
