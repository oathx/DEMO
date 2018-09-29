module("h5Root", singleton)

function init(self)
	self.entity_ = h5Entity.new()
end

function startup(self, config)
	if not self.entity_ then
		return false
	end

	self.tcp_server_ = self.entity_:add_component(h5Server, config.ip, config.port)
	if not self.tcp_server_ then
		return false
	end

	local protocol = Protocol.new()
	if protocol then
		RpcParse.parse_protocol(protocol)
	end

	Rpc:init(self.tcp_server_)
	
	return true
end

function get_server(self)
	return self.tcp_server_
end

function run(self)
	if not self.entity_ or not self.tcp_server_ then
		return false
	end

	uvcore.run()

	return true
end

function shutdown(self)
	if not self.entity_ or not self.tcp_server_ then
		return false
	end

	uvcore.stop()

	return true
end