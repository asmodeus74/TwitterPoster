TwitterPoster
=============

Small twitter poster utiliy

Twitter poster is a simple quick & dirty tweet sender application.. 
It takes a file name as parameter and assumes every line is a two line message seperated by a colon ":"
Splits the line with colon and make it two line message.. Also takes an optional parameter tags and appends tags to generated mesaage.

I used it for my simple Esperanto Dictionary project.
https://twitter.com/EsperantoDict

Sample usage of program
TwitterPoster.exe -i [input file name] -t "#tag1 #tag2"

On execution program takes a line, and generates and sends message, and writes to disk on which line posted last, and takes the next line on the next run.
