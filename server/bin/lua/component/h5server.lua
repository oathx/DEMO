module("h5Server", package.seeall)
setmetatable(h5Server, {__index=h5Component})

function new(entity, ipaddress, port)
	local comp = {
	}
	setmetatable(comp, {__index=h5Server})
	comp:awake(entity, ipaddress, port)

	return comp
end

function awake(self, entity, ipaddress, port)
	h5Component.awake(self, entity)

	self.ipaddress_ 	= ipaddress
	self.port_ 			= port
	self.connect_mgr_	= self.entity_:add_component(h5ConnectManager)
end

function start(self)
	if self.ipaddress_ and self.port_ then
		self.server_ = self:create_server(self.ipaddress_, self.port_, function(client)
			self:on_server_accept(client)
		end)

		if not self.server_ then
			ERROR("Can't create tcp server ip(%s:%s)", self.ipaddress_, self.port_)
			return
		else
			INFO("h5TcpServer start ip(%s) port(%s)",
				self.ipaddress_, self.port_)
		end
	end
end

function get_ipaddress(self)
	return self.ipaddress_
end

function get_port(self)
	return self.port_
end

function create_server(self, host, port, concb)
	local tcp_server = uvcore.new_tcp()
	if tcp_server then
		tcp_server:bind(host, port)
		
		tcp_server:listen(128, function(err)
			assert(not err, err)

			local sock = uvcore.new_tcp()
			if sock then
				tcp_server:accept(sock)

				if concb then
					concb(sock)
				end
			end
		end)

		return tcp_server
	end
end

function on_server_accept(self, sock)
	local address = sock:getsockname()

	local conn = h5Entity.new()
	if conn then
		local comp = conn:add_component(h5Connect, address.ip, address.port)
		if comp then
			comp:set_socket(sock)
		end

		INFO("on_server_accept %s:%d eid(%d) cid(%d)", 
			address.ip, address.port, conn:eid(), conn:cid())

		if self.connect_mgr_ then
			self.connect_mgr_:add_connect(conn)
		end
	end
end
