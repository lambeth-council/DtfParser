## Synopsis

This project loads a file in DTF7.3 format into a database. 

DTF7.3 is the Data Transfer Format specification used by local councils to export their address data from their LLPG, in a format matching BS 7666:2006.

The data is loaded into a set of database tables - BLPUs, LPIs and Street Descriptors making it easy to list all the addresses and use sql to query them.

The code supports delta files, using DTF lines are marked as Insert, Update or Delete.

## Code Example

The project uses a command line Visual Studio 2015 project with .Net 4.5.2 and Sql Server 2008 R2 and has been designed to be easy to port to different versions/technologies

Once compiled, go to the directory containing the executable (usually bin\debug) and run a command of the format

parsedtf load "[ConnectionString]" "[FullFileName]"

ConnectionString - the sql connection string

FullFileName - the full file name including drive and path

e.g.

parsedtf load "data source=localhost;initial catalog=DTF;integrated security=True;" "C:\DtfFiles\llpg.csv"

There are a few basic test files in the TestDtfFiles folder to try out

## Motivation

I couldn't find any open source code to do this, yet it's used by all councils in the UK. 

Once the addresses are in a database then it opens up the possibility of
* health checks on the LLPG data, making sure fields are populated correctly
* bulk fuzzy address matching to get UPRNs from free typed address text
* address lookup widgets 

## Installation

Create a blank Sql database

Run Db\Schema.sql to create the schema

Run Db\Data.sql to populate the tables

Compile the project

Run a command line as in the Code Example above

## API Reference

Theres only two functions currently

parsedtf load "[ConnectionString]" "[FullFileName]" - loads the DTF file into the database

parsedtf clearall "[ConnectionString]" - resets the database

## Database 

Tables
* DtfLine - contains all lines from all files that have been loaded, acts like a log. 

* BLPU - Basic Land and Property Unit - DTF Record Identifier 21
* LPI - Land and Property Identifier - DTF Record Identifier 24
* StreetDescriptor - Street Descriptor - DTF Record Identifier 15
* StreetRecord - Street Record - DTF Record Identifier 11

* RecordIdentifier - a list of all the DTF Record Identifiers
* Version - a record is generated for each file loaded, and versionid used to refer back to filename / date time stamps

Views
* vwAddresses - contains all the address fields plus record statuses and BLPU Codes
* vwLpi - LPI fields with concatenated PAON and SAON number and description fields
* vwDtfLine - all the DTF lines that have been loaded along with datestamps and source filenames

Stored Procedures
* spTruncateEverything - resets database 

## Tests

Run these from the command line replaceing <path> with your path
* parsedtf load "data source=localhost;initial catalog=DTF;integrated security=True;" "C:\<path>\ParseDtf\TestDtfFiles\insert.csv"
* Check the street record table - it should have 3 records in it

* parsedtf load "data source=localhost;initial catalog=DTF;integrated security=True;" "C:\<path>\ParseDtf\TestDtfFiles\update.csv"
* The record with USRN = 20900161 has been updated so Street_Start_x = 666666.00, also it's versionId has increased

* parsedtf load "data source=localhost;initial catalog=DTF;integrated security=True;" "C:\<path>\ParseDtf\TestDtfFiles\delete.csv"
* The record with USRN = 20900162 has been deleted 

* Check the version table and you can see what files were run and when. Messsages field gives you a full breakdown or insert/deletes. Note an update is just a delete followed by an insert.

## Contributors

Just contact me through Github

## License

MIT