module("LoginSystemEntity", package.seeall)
setmetatable(LoginSystemEntity, {__index=BaseEntity})

function new(id)
	local entity = {
	}
	setmetatable(entity, {__index=LoginSystemEntity})
	entity:Init(id)

	return entity
end

function Init(self, id)
	BaseEntity.Init(self, id)

	self.userName = "Super"
	self.password = "Super"
end

function GetUserName(self)
	return self.userName
end

function GetPassword(self)
	return self.password
end