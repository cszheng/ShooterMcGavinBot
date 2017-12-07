namespace Tests.Main
{
    public class TestsBase
    {
        protected string _testDir;
        protected string _projectDir;

        public TestsBase() 
        {
            //project directory, where the .csproj is at
            _testDir = "../../..";
            _projectDir = $"{_testDir}/../ShooterMcGavinBot";
        }
    }
}
