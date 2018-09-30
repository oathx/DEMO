module("TcpServer", package.seeall)
extendsClass(TcpServer, Component)

function Awake(self, entity)
    Component.Awake(self, entity)
    
    self.clients = {}
end

function Start(self)
    Component.Start(self)
end

-- create tcp server
function CreateServer(self, host, port, max)
    self.server = uvcore.new_tcp()
    self.server:bind(host, port)
  
    self.server:listen(max, function(err)
        -- Make sure there was no problem setting up listen
        assert(not err, err)

        -- Accept the client
        local client = uvcore.new_tcp()
        if client then
            self.server:accept(client)

            -- generate a new client session
            self:OnAccept(client)
        end
    end)

    INFO("TcpServer %s:%d created", host, port)

    return uvcore.run()
end

-- create a new client session
function CreateClient(self, client)
    local entity = Entity()
    if entity then
        local component = entity:AddComponent(TcpClient, client)

        local id = entity:GetID()
        self.clients[id] = entity

        return entity, component
    end
end

-- close server
function DestroyServer(self)
    if self.server then
        self.server:shutdown()
        self.server:close()
    end

    for id, client in pairs(self.clients) do
        client:Destroy()
    end

    uvcore.loop_close()
end

function GetServer(self)
    return self.server
end

function OnAccept(self, client)
    local peer = client:getpeername()
    INFO("TcpClient Start peer family %s [%s:%s] client entity(%s)",
        peer.family, peer.ip, peer.port,  self.entity:GetID())
end

