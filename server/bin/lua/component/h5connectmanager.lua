module("h5ConnectManager", package.seeall)
setmetatable(h5ConnectManager, {__index=h5Component})

function new(entity)
	local connect = {
	}
	setmetatable(connect, {__index=h5ConnectManager})
	connect:awake(entity)

	return connect
end

function awake(self, entity)
	h5Component.awake(self, entity)

	self.conn_list_ = {}
end

function add_connect(self, conn)
	local eid = conn:eid()
	if not self.conn_list_[eid] then
		self.conn_list_[eid] = conn
	end
end