module("EntityManager", mkSingleton)

function Init(self)
	self.entitys = {
	}
end

function Startup(self, factory)
	for id, func in pairs(factory) do
		local entity = func(id)
		ELOG("Startup install defualt entity id(%d) name(%s)", id, entity:Name())
		if entity then
			self:AddEntity(entity)
		end
	end
end

function Shutdown(self)
	for id, entity in pairs(self.entitys) do
		if entity then
			entity:Destroy()
		end
	end

	self.entitys = {}
end

function AddEntity(self, entity)
	if entity then
		local id = entity:ID()
		if not self.entitys[id] then
			self.entitys[id] = entity
		end
	end
end

function GetEntity(self, id)
	return self.entitys[id]
end

function RemoveEntity(self, id)
	local entity = self.entitys[id]
	if entity then
		entity:Destroy()
	end
end
