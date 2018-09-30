module("RenderShape", package.seeall)
setmetatable(RenderShape, {__index=BaseComponent})

function new(entity, go)
	local rs = {
	}
	setmetatable(rs, {__index=RenderShape})
	rs:Init(entity, go)

	return rs
end

function Init(self, entity, go)
	BaseComponent.Init(self, entity)

	-- create shape model
	self.model = BaseModel.new(go)
end

function GetModel(self)
	return self.model
end

function OnDestroy(self)
	if self.model then
		self.model:Release()
	end
end