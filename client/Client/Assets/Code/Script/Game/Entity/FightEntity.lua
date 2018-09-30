module("FightEntity", package.seeall)
setmetatable(FightEntity, {__index=BaseEntity})

function new(id)
	local entity = {
	}

	setmetatable(entity, {__index=FightEntity})
	entity:Init(id)

	return entity
end

function Init(self, id)
	BaseEntity.Init(self, id)
end
