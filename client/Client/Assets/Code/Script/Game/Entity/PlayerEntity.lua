module("PlayerEntity", package.seeall)
setmetatable(PlayerEntity, {__index=BaseEntity})

function new(id)
	local entity = {
	}

	setmetatable(entity, {__index=PlayerEntity})
	entity:Init(id)

	return entity
end

function Init(self, id)
	BaseEntity.Init(self, id)
end

function CreateShape(self, go)
	if not self.shape then
		self.shape = self:AddComponent(RenderShape, go)
	else
		ELOG("create entity shape failed")
	end

	return self.shape
end

function GetShape(self)
	return self.shape
end

function DestroyShape(self)
	if self.shape then
		self:RemoveComponent(self.shape)
	end

	-- destroy player entity shape
	self.shape = nil
end