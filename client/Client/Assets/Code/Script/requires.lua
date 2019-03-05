-- config script
require("resmng")
-- common lua script
require("Common/Class")
require("Common/Utility")
require("Common/RpcType")
require("Common/RpcStruct")
require("Common/Rpc")
require("Common/LuaBehaviour")
require("Common/BaseEntity")
require("Common/BaseComponent")
require("Common/EventModule")
require("Common/EventBehaviour")
require("Common/EventDispatch")
require("Common/GamePlugin")
require("Common/EntityManager")
require("Common/PluginManager")
require("Common/UIWidget")
require("Common/UISystem")
require("Common/BaseModel")
require("Common/BaseStateMachine")

-- require ui lib
require("UI/UIConfigure")
require("UI/UIModel")
require("UI/UIScreen")
require("UI/UITask")
-- logic lua script


require("Game/Component/RenderShape")

require("Game/Entity/LoginSystemEntity")
require("Game/Entity/MenuSystemEntity")
require("Game/Entity/TaskSystemEntity")
require("Game/Entity/FightSystemEntity")
require("Game/Entity/PlayerEntity")
require("Game/Entity/TaskEntity")

require("Game/NetEvent")
require("Game/ProtocolImp/ProtocolNormal")
require("Game/System/SystemPlugin")
require("Game/System/LoginSystem")
require("Game/System/MenuSystem")
require("Game/System/TaskSystem")
require("Game/System/FightSystem")
require("Game/RootContext")
require("Game/GameManager")
