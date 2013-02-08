NServiceBus.Host.exe /install /serviceName:"Rem NServiceBus Host Service"
sc start "Rem NServiceBus Host Service"
::NServiceBus.Host.exe /uninstall /serviceName:"Rem NServiceBus Host Service"