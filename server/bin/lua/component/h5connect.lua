module("h5Connect", package.seeall)
setmetatable(h5Connect, {__index=h5Component})

function new(entity, ip, port)
	local connect = {
	}
	setmetatable(connect, {__index=h5Connect})
	connect:awake(entity, ip, port)

	return connect
end

function awake(self, entity, ip, port)
	h5Component.awake(self, entity)
	self.session_ = self.entity_:add_component(h5Session, ip, port)

	self.buffer_ = uvcore.new_buffer(1024 * 10, function(data, size)
			local pack = uvcore.new_packet(data)
			if pack then
				self:on_sock_packet(pack)
			end
		end)

	self.ipaddress_ = ip
	self.port_ = port
	self.type_ = resmng.CONNECT_NONE
	self.sock_ = nil
end

function get_ipaddress(self)
	return self.ipaddress_
end

function get_port(self)
	return self.port_
end

function get_type(self)
	return self.type_
end

function set_socket(self, sock)
	self.sock_ = sock
	if not self.sock_ then
		return false
	end

	self.sock_:read_start(function(err, chunk)
		assert(not err, err)

		if self.buffer_ then
			self.buffer_:append(chunk)
		end
	end)
end

function send(self, data)
	if self.sock_ then
		return uvcore.write(self.sock_, data)
	end

	return 0
end

function on_sock_packet(self, packet)
	local len = packet:read_int()
	local cmd = packet:read_int()

	local rf = RpcParse.sf(cmd)
	if rf then
		local args={}
		for i,v in ipairs(rf.args) do
			local rt = RpcType[v.t]
			args[i] = rt._read(packet)
		end

		local func = self.session_[rf.name]
		if func then
			func(self.session_, unpack(args))
		end
	end
end