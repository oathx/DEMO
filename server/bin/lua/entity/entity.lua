module("Entity", package.seeall)
extendsClass(Entity, Object)

function Awake(self, uid)
    self.components = {}
end

function AddComponent(self, component, ...)
    if component then
        local newComp = component.new(self, ...)
        if newComp then
            newComp:Start()
            
            table.insert(self.components, newComp)
            
            return newComp
        end
    end
end

function RemoveComponent(self, component)
    for idx, comp in ipairs(self.components) do
        if comp:GetTypeName() == component._NAME then
            comp:Destroy()
            table.remove(self.components, idx)
            break
        end
    end
end

function GetComponent(self, component)
    for idx, comp in ipairs(self.components) do
        if comp:GetTypeName() == component._NAME then
            return comp
        end
    end
end

function Destroy(self)
    for idx, comp in ipairs(self.components) do
        comp:Destroy()
    end
    
    self.components = {}
end