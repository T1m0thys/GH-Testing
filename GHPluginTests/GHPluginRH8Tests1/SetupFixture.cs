namespace GHPluginRH8Tests1
{
    [SetUpFixture]
    public sealed class SetupFixture : Rhino.Testing.Fixtures.RhinoSetupFixture
    {
        public override void OneTimeSetup()
        {
            base.OneTimeSetup();
            // your custom setup
        }

        public override void OneTimeTearDown()
        {
            base.OneTimeTearDown();
            // you custom teardown
        }
    }
}
