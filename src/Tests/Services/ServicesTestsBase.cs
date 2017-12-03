using System;
using System.IO;
using System.Collections.Generic;
using Moq;
using ShooterMcGavinBot.Common;


namespace Tests.Main
{
    public class ServicesTestsBase: TestsBase
    {
        protected Mock<IBotStringsContainer> _mockBotStringsCntr;
        protected Dictionary<String, String> _commonBotStrings; 

        public ServicesTestsBase() 
        {
            //mock the common botstrings
            _commonBotStrings = new Dictionary<String, String>();
            _commonBotStrings.Add("section_breaks", "**--------------------------------------------------------------------------------**");
            _commonBotStrings.Add("command_header", "__**Commands:**__");
            _commonBotStrings.Add("command_description", "**{0}** - *{1}*");
            _commonBotStrings.Add("method_description", "**{0}** - *{1}*");
            _commonBotStrings.Add("parameter_description", "    __{0}__ - *{1}*");
            //make mock object           
            _mockBotStringsCntr = new Mock<IBotStringsContainer>();
            _mockBotStringsCntr.Setup(x => x.getString("common", It.IsAny<String>()))
                               .Returns((String x, String y) => { return _commonBotStrings[y]; });
        }
    }
}
