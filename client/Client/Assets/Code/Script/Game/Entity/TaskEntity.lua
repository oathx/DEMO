module("TaskEntity", package.seeall)
setmetatable(TaskEntity, {__index=BaseEntity})

function new(id)
	local entity = {
	}

	setmetatable(entity, {__index=TaskEntity})
	entity:Init(id)

	return entity
end

function Init(self, id)
	BaseEntity.Init(self, id)

	self.prop = resmng.propTaskById(id)
	if not self.prop then
		ELOG("Can't find task config(%d)", id)
		return
	end
end

function GetSceneID(self)
	return self.prop.Scene or 0
end