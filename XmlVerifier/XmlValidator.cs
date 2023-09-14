namespace XmlVerifier
{
    public class XmlValidator
    {
        /// <summary>
        /// Static method for validating an XML string
        /// 
        /// If all opening tags have a closing tag the XML string is determined as valid
        /// If an opening tag has an attribute that constitutes to an invalid XML tag
        /// </summary>
        /// <param name="xml">The XML string</param>
        /// <returns>True if the XML string is valid, False if not.</returns>
        public static bool DetermineXml(string xml)
        {
            // Default return value is false
            var result = false;
            
            // Validate the validator's input
            if(XmlValidator.IsValidInput(xml))
            {
                #region Why a stack for our tracker?

                // We are using a stack as our data structure
                // Reason being:
                // A list would take too much memory
                // An array needs to have the length determined
                // We can't use a queue because:
                    // Since xml strings are nested we need to be able to get what ever we push in a LIFO fashion
                    // Because the last xml tag name to be pushed should be the innermost one and as we find closing
                    // tags we pop from the inner tag to the outermost tag

                #endregion
                var foundXmlOpeningTagNames = new Stack<string>();

                // Our main parser index to be used as we traverse through the whole XML string
                var parserIndex = 0;

                // We only parse the length of the string hence the condition
                while (parserIndex < xml.Length)
                {
                    // Every iteration we try to find any XML tags, which is indicated by a starting <
                    if (xml[parserIndex] == '<')
                    {
                        #region What is this sub-parser for?

                        // This variable is used so we can parse through all the characters in the string after the tag we've found
                        // The ultimate goal of doing a sub-parse (i.e. parsing only a smaller part of the xml string) is:
                        // To find the closing > of the potential XML tag we just found

                        #endregion
                        var potentialTagSubParserIndex = parserIndex + 1;

                        #region Why do we use these conditions for the sub-parser?

                        // As mentioned previously, we need to parse and check for the closing > of the potential XML tag
                        // We give this subparser the ability to traverse through the whole XML string
                        // But once we found a closing > it could technically mean we found the tag's closing and can terminate the loop

                        #endregion
                        while (potentialTagSubParserIndex < xml.Length && xml[potentialTagSubParserIndex] != '>')
                        {
                            // Tag parser index iterates as long as it should depending on the XML tag's name
                            potentialTagSubParserIndex++;
                        }

                        #region Why do we break the loop prematurely?

                        // If the while loop terminates because the opening tag parse index is equal to input length..
                        // That means we ran through the end of the input and found a tag that has a < without any closing >
                        // Which means the input is invalid

                        #endregion
                        if (potentialTagSubParserIndex == xml.Length)
                        {
                            // Invalid input means we break and return false
                            return false;
                        }

                        #region Why do we need to use substring on the input?

                        // Moving on, now that we've determined that we're dealing with a proper XML tag...
                        // We need to extract the tag from the xmlString
                        // We use substring that starts from the current parser index, we use + 1 to skip over the opening <
                        // We then end the substring query by skipping over the closing >
                        // Since we know the position of the opening < we can subtract it from the sub parser index which is a bit more ahead
                        // We then subtract another 1 as we want to skip over the closing >

                        #endregion
                        var xmlTagName = xml.Substring(parserIndex + 1, potentialTagSubParserIndex - parserIndex - 1);

                        #region Why do we have to check for the / character?

                        // Now that we have the content of the tag that was wrapped in < >
                        // We need to figure out if it's a closing XML tag which usually starts with a /
                        // We do this so we can pop the tag from our tracker (i.e. the stack)

                        #endregion
                        if (xmlTagName.StartsWith("/"))
                        {
                            // Popping the LATEST opening tag name from our tracker
                            var latestXmlOpeningTagName = foundXmlOpeningTagNames.Pop();

                            #region Why do we break the loop prematurely (again)?

                            // We now try to match the tag name...
                            // We use substring on the xml tag we just extracted and skip over the /
                            // Since closing tags start with a / we extract the substring from index 1

                            #endregion
                            if (latestXmlOpeningTagName != xmlTagName.Substring(1))
                            {
                                // If the openingXmlTag does not match the found closing tag we break and return false
                                return false;
                            }
                        }
                        else
                        {
                            // If the tag name we found wasn't a closing tag, we push it to our tracker
                            foundXmlOpeningTagNames.Push(xmlTagName);
                        }

                        // Since we found a tag and know the index we can skip over that index for our main parser index counter
                        parserIndex = potentialTagSubParserIndex + 1;

                        // Continue the loop and try finding the next potential tag
                        continue;
                    }

                    // If the main parser has not found an opening < we just continue to iterate over our string by 1
                    parserIndex++;
                }

                #region Why is this our result condition?

                // If all tags from the inner most one to the outermost ones have opening and closing ones
                // We should be guaranteed an empty stack and we can return true
                #endregion
                result = foundXmlOpeningTagNames.Count == 0;
            }
            return result;
        }

        /// <summary>
        /// Validates the input string of this validator class
        /// </summary>
        /// <param name="xmlString">The xml string input</param>
        /// <returns>True if it's potentially a valid xml string input, False if it's empty, null, OR doesn't even have an opening '<' character</returns>
        private static bool IsValidInput(string xmlString)
        {
            // The input xml string can be invalid xml but it should satisfy the minimum requirements for the validator to execute
            return !string.IsNullOrWhiteSpace(xmlString) && xmlString.Contains('<');
        }
    }
}
