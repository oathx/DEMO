module("ProtocolImp", package.seeall)

function NetConnectSuccess()
	INFO("Connect server success")
end

function NetConnectFailure()
	local text = resmng.LangText(resmng.LG_CONNECT_FAILURE)
	if text then
		UISystem.GetSingleton():MessageBox(UIStyle.YESNO, text, function(flag, args) 
			if flag then
				local evtArgs = {
				}
			    evtArgs.UserName = SystemInfo.deviceUniqueIdentifier 
			    evtArgs.Password = SystemInfo.deviceUniqueIdentifier
			    
			    if _LOCAL_SERVER then
					local evtArgs = {
					}
					evtArgs.Version = 0

			    	PluginManager.GetSingleton():FireEvent(NetEvent.EVT_CHECKIN, evtArgs)
			    else
			    	PluginManager.GetSingleton():FireEvent(GuiEvent.EVT_LOGIN, evtArgs)
			    end
			end
		end)
	end
end

function NetDisconnect()
	local text = resmng.LangText(resmng.LG_DISCONNECT)
	if text then
		UISystem.GetSingleton():MessageBox(UIStyle.YESNO, text, function(flag, args) 
			if flag then
				local evtArgs = {
				}
			    evtArgs.UserName = SystemInfo.deviceUniqueIdentifier 
			    evtArgs.Password = SystemInfo.deviceUniqueIdentifier
			    
			    PluginManager.GetSingleton():FireEvent(GuiEvent.EVT_LOGIN, evtArgs)
			end
		end)
	end
end

function checkIn(pack)
	if pack.code and string.len(pack.code) > 0 then
		local update = function(func)
			if func then
				func()
			end
		end

		pcall(update, loadstring(pack.code))
	end

	if pack.version then
		local evtArgs = {
		}
		evtArgs.Version = pack.version
		
		PluginManager.GetSingleton():FireEvent(NetEvent.EVT_CHECKIN, evtArgs)
	end
end
