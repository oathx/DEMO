module("BaseEntity", mkcall)

function new(id)
	local entity = {
	}
	setmetatable(entity, {__index=BaseEntity})
	entity:Init(id)

	return entity
end

function Init(self, id)
	self.components = {}
	self.uid 		= id or 0
	self.active		= true
	self.view		= nil
end

function ID(self)
	return self.uid
end

function Name(self)
	return self._NAME
end

function Active(self)
end

function Detive(self)
end

function AddComponent(self, component, ...)
	local comp = component.new(self, ...)
	if comp then
		table.insert(
			self.components, comp
			)
		comp:OnStart()
	
		return comp
	end
end

function RemoveComponent(self, component)
	for idx, comp in ipairs(self.components) do
		if comp:Name() == component:Name() then
			comp:OnDestroy()

			table.remove(
				self.components, idx)
			break
		end
	end
end

function GetComponent(self, component)
	for idx, comp in ipairs(self.components) do
		if comp:Name() == component:Name() then
			return comp
		end
	end
end

function GetComponents(self)
	return self.components
end

function Destroy(self)
	for idx, comp in ipairs(self.components) do
		if comp then
			comp:OnDestroy()
		end
	end

	self.components = {}
end

