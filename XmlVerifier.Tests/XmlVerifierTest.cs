namespace XmlVerifier.Tests
{
    [TestClass]
    public class XmlVerifierTest
    {
        [TestMethod]
        public void DetermineXml_ValidInput_True()
        {
            var input = "<Design><Code>hello world</Code></Design>";
            Assert.IsTrue(XmlValidator.DetermineXml(input));
        }

        [TestMethod]
        public void DetermineXml_InvalidInput_Length0Input_False()
        {
            var invalidInput = "";
            Assert.IsFalse(XmlValidator.DetermineXml(invalidInput));
        }

        [TestMethod]
        public void DetermineXml_InvalidInput_NullInput_False()
        {
            var invalidInput = default(string);
            Assert.IsFalse(XmlValidator.DetermineXml(invalidInput));
        }

        [TestMethod]
        public void DetermineXml_InvalidInput_NotPotentialXml_False()
        {
            var invalidInput = "invalid input";
            Assert.IsFalse(XmlValidator.DetermineXml(invalidInput));
        }

        [TestMethod]
        public void DetermineXml_InvalidInput_PeopleTagOutsideOfDesignTagAndNoClosingTag_False()
        {
            var invalidInput = "<Design><Code>hello world</Code></Design><People>";
            Assert.IsFalse(XmlValidator.DetermineXml(invalidInput));
        }

        [TestMethod]
        public void DetermineXml_InvalidInput_InvalidClosingTagsForMostElements_False()
        {
            var invalidInput = "<People><Design><Code>hello world</People></Code></Design>";
            Assert.IsFalse(XmlValidator.DetermineXml(invalidInput));
        }

        [TestMethod]
        public void DetermineXml_InvalidInput_OpeningTagHasAnAttribute_False()
        {
            var invalidInput = "<People age=”1”>hello world</People>";
            Assert.IsFalse(XmlValidator.DetermineXml(invalidInput));
        }
    }
}