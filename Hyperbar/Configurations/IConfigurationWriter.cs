﻿namespace Hyperbar.Configurations;

public interface IConfigurationWriter<TConfiguration> where TConfiguration : class
{
    void Write(string section, TConfiguration args);
}
