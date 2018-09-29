module("GameApp", package.seeall)
extendsClass(GameApp, TcpServer)

function Awake(self, entity)
    TcpServer.Awake(self, entity)
    
    self.clients = {}
end

function Start(self)
    TcpServer.Start(self)

    self.logonApp = uvcore.new_tcp()
    if self.logonApp then
        self.logonApp:connect("127.0.0.1", 9527, function(err)
            assert(not err, err)
            INFO("connect logonApp OK")
        end)        
    else
        ERROR("Can't create tcp connect")
    end
end

function OnAccept(self, client)
    TcpServer.OnAccept(self, client)
end