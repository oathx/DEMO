module("h5Entity", package.seeall)

function new(eid)
	local entity = {
	}
	setmetatable(entity, {__index=h5Entity})
	entity:init(eid)

	return entity
end

function init(self, eid)
	self.eid_ = eid or tools.genid()
	self.cid_ = tools.genid()
	self.components_ = {}
	self.active_ = true

	self:set_active(true)
end

function eid(self)
	return self.eid_
end

function cid(self)
	return self.cid_
end

function set_active(self, active)
	if self.active_ ~= active then
		self.active_ = active
	end
end

function is_active(self)
	return self.active_
end

function add_component(self, component, ...)
	local comp = component.new(self, ...)
	if comp then
		table.insert(
			self.components_, comp
			)
		comp:start()
	
		return comp
	end
end

function remove_component(self, component)
	for idx, comp in ipairs(self.components_) do
		if comp:classid() == component:classid() then
			comp:destroy()

			table.remove(
				self.components_, idx)
			break
		end
	end
end

function get_component(self, component)
	for idx, comp in ipairs(self.components_) do
		if comp:classid() == component:classid() then
			return comp
		end
	end
end

function get_components(self)
	return self.components_
end
