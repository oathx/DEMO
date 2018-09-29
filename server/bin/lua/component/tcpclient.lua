module("TcpClient", package.seeall)
extendsClass(TcpClient, Component)

function Awake(self, entity, client)
    Component.Awake(self, entity)

    self.client     = client
    self.peer       = client:getpeername()

    self.buffer     = uvcore.new_buffer(1024)
    self.clientType = ClientType.NONE
end

function Start(self)
    INFO("TcpClient Start peer family %s [%s:%s] client entity(%s) buffer %s",
        self.peer.family, self.peer.ip, self.peer.port,  self.entity:GetID(), tostring(self.buffer))

    self.client:read_start(function(err, chunk)
        assert(not err, err)

        if chunk then
            if self.clientType == ClientType.NONE then
                local uprequest = chunk .. '\r\n'
                local protocols = {
                }

                local response, protocol = tools.accept_upgrade(uprequest, protocols)
                if not response then
                    print('Handshake failed, Request:', uprequest)
                else

                end
            else

            end
        else
            self:Shutdown()
        end
    end)
end

function Shutdown(self)
    INFO("TcpClient Shutdown family %s [%s:%s] client entity(%s) buffer %s",
        self.peer.family, self.peer.ip, self.peer.port,  self.entity:GetID(), tostring(self.buffer))

    if self.client then
        self.client:shutdown()
    end

    self:Destroy()
end

function Destroy(self)
    if self.buffer then
        uvcore.del_buffer(self.buffer)
    end
    
    if self.client then
        self.client:close()
    end

    self.buffer = nil
    self.client = nil
end


