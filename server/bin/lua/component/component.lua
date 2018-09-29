module("Component", package.seeall)
extendsClass(Component, Object)

function Awake(self, entity)
    self.entity = entity
    self.timers = {}
end

function Start(self)
end

function Update(self)
end

function Shutdown(self)
end

function Destroy(self)
end

function SetTimeOut(self, time, callabck)
    local timer = uvcore.new_timer()
    if timer then
        timer:start(time, 0, function() 
            timer:stop()
            timer:close()

            if callabck then
                callabck()
            end
        end)
    end
end

function GetEntity(self)
    return self.entity
end
