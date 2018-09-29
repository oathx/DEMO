module("LogonApp", package.seeall)
extendsClass(LogonApp, TcpServer)

function Awake(self, entity)
    TcpServer.Awake(self, entity)
    
    self.clients = {}
end

function Start(self)
    TcpServer.Start(self)
end

function OnAccept(self, client)
    TcpServer.OnAccept(self, client)
end