module("h5Component", package.seeall)

function new(entity, ...)
	local comp = {
	}
	setmetatable(comp, {__index=h5Component})
	comp:awake(entity, ...)

	return comp
end

function awake(self, entity, ...)
	self.entity_ = entity
	self.enabled_ = true
end

function start(self)
end

function update(self)
end

function destroy(self)
end

function enabled(self)
end

function disable(self)
end

function classid(self)
	return self._NAME
end

function get_entity(self)
	return self.entity_
end

function set_enabled(self, enabled)
	if self.enabled_ ~= enabled then
		self.enabled_ = enabled

		if self.enabled_ then
			self:enabled()
		else
			self:disable()
		end
	end
end

function is_enabled(self)
	return self.enabled_
end






