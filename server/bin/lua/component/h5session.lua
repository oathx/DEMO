module("h5Session", package.seeall)
setmetatable(h5Session, {__index=h5Component})

function new(entity, ip, port)
	local session = {
	}
	setmetatable(session, {__index=h5Session})
	session:awake(entity, ip, port)

	return session
end

function awake(self, entity)
	h5Component.awake(self, entity)
end

function checkIn(self, pack)
	for k, v in pairs(pack) do
		print(k, v)
	end

	local ci = {
		version = "0.0.1.0001",
		code = ""
	}

	local conn = self.entity_:get_component(h5Connect)
	if conn then
		Rpc:checkIn(conn, ci)
	end
end
