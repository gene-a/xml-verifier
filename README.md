# Requirements
Given an XML string write a function that returns true if the string is a valid XML string; otherwise return false.
- A string is a valid XML string if it satisfies the following rules:
    - Each starting element must have a corresponding ending element
    - Nesting of elements within each other must be well nested, which means start first must end last. For example, <tutorial><topic>XML</topic></tutorial> is a correct way of nesting but <tutorial><topic>XML</tutorial></topic> is not
To simplify the question, we treat a pair of  opening tag and closing tags as matching only if the strings in both tags are identical. We don’t need to parse extra components like attributes in the XML tag. 

# Restrictions
Usage of `System.XML` and `Regular Expression` are **prohibited**
Function signature should be: `bool DetermineXml(string xml)`