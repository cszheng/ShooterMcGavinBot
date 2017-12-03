using System.Collections.Generic;
using Moq;
using ShooterMcGavinBot.Common;

namespace Tests.Main
{
    public class ServicesTestsBase: TestsBase
    {
        protected Mock<IBotStringsContainer> _mockBotStringsCntr;
        protected Dictionary<string, string> _commonBotStrings; 

        public ServicesTestsBase() 
        {
            //mock the common botstrings
            _commonBotStrings = new Dictionary<string, string>();
            _commonBotStrings.Add("section_breaks", "**--------------------------------------------------------------------------------**");
            _commonBotStrings.Add("command_header", "__**Commands:**__");
            _commonBotStrings.Add("command_description", "**{0}** - *{1}*");
            _commonBotStrings.Add("method_description", "**{0}** - *{1}*");
            _commonBotStrings.Add("parameter_description", "    __{0}__ - *{1}*");
            //make mock object           
            _mockBotStringsCntr = new Mock<IBotStringsContainer>();
            _mockBotStringsCntr.Setup(x => x.getString("common", It.IsAny<string>()))
                               .Returns((string x, string y) => { return _commonBotStrings[y]; });
        }
    }
}
