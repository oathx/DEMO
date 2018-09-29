require("util")

function do_require(dbg)	
	if dbg then
		local ary_path = tools.string_split(package.path, ";")
		for k, v in pairs(ary_path) do
			print(k, v)
		end
	end
	
    for name, v in pairs(package.loaded) do
        package.loaded[name] = nil
    end

	local server_config_script = arg[1]
	if server_config_script then
		dofile(server_config_script)
	end

    local ret, msg = pcall(require, "requires")
    if not ret then
        print("critial error! requires failed: ", msg)
    end
	
    return ret
end

function main()
	if do_require(false) then
		local client = uvcore.new_tcp()
		if client then
			print(ServerConfigure.ip, ServerConfigure.port)
			client:connect(ServerConfigure.ip, ServerConfigure.port, function(err) 
				assert(not err, err)
				client:read_start(function(errmsg, chunk)
					
				end)
			end)

			uvcore.run()
		end
	end
end

