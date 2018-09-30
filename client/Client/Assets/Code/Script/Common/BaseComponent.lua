module("BaseComponent", package.seeall)

function new(entity, ...)
	local comp = {
	}
	setmetatable(comp, {__index=BaseComponent})
	comp:Init(entity, ...)

	return comp
end

function Init(self, entity, ...)
	self.entity 	= entity
	self.enabled 	= true
	self.active		= true
end

function Name(self)
	return self._NAME
end

function GetEntity(self)
	return self.entity
end

function SetEnabled(self, enabled)
	if self.enabled ~= enabled then
		self.enabled = enabled

		if self.enabled then
			self:OnEnabled()
		else
			self:OnDisable()
		end
	end
end

function IsEnabled(self)
	return self.enabled
end

function OnStart(self)
end

function OnUpdate(self)
end

function OnDestroy(self)
end

function OnEnabled(self)
end

function OnDisable(self)
end






