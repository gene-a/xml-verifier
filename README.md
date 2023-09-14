# XML-Verifier
## Requirements
Given an XML string write a function that returns true if the string is a valid XML string; otherwise return false.
- An XML string is valid if it satisfies the following:
    - Each starting element must have a corresponding ending element.
    - Nested elements should have matching opening and ending tags.
- For simplicity we treat a pair of opening and closing tags as a "match" only if the strings in both tags are identical.
- We don’t need to parse extra components like attributes in the XML tag. 

## Restrictions
- Usage of `System.XML` and `Regular Expression` are **prohibited**.
- Function signature should be: `bool DetermineXml(string xml)`.
