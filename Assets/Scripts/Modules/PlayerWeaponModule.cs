namespace Entities.Modules
{
    /// <summary>
    /// A module that handles player input and shoots if the fire button is pressed.
    /// </summary>
    public class PlayerWeaponModule : WeaponModule
    {
        private IInputModule inputModule;       // Cached player input module.
        private IModuleHandler handler;         // Cached module handler.

        private bool shotEventTriggered;        // Was the shoot button pressed?

        /// <summary>
        /// Initializes the module with the passed ModuleHandler.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public override void Init(IModuleHandler handler)
        {
            base.Init(handler);
            
            this.handler = handler;
            if (handler.AreAllModulesInitialized)
                OnInitModulesEnd();
            else this.handler.OnInitModulesEnd += OnInitModulesEnd;
        }

        /// <summary>
        /// Gets the input module and subscribes to the input button action event.
        /// </summary>
        private void OnInitModulesEnd()
        {
            inputModule = handler.GetModule<IInputModule>();
            inputModule.OnButtonAction += ButtonPressed;
        }

        /// <summary>
        /// Shoots if the fire button has been pressed.
        /// </summary>
        protected override void Update()
        {
            base.Update();
            if (shotEventTriggered)
                Shoot();
        }

        /// <summary>
        /// Checks if the button type is a shoot button and sets the shoot trigger to the button state.
        /// </summary>
        private void ButtonPressed(ButtonType buttonType, bool pressed)
        {
            if (buttonType != ButtonType.Action)
                return;

            shotEventTriggered = pressed;
        }
    }
}
