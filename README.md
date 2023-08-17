## Hot Stuff
A web / desktop / mobile application designed to help users format fire insurance claims. 

### Objective
To help people better recover/prepare for the arduous process of receiving fair value for their lost belongings in fire insurance claims.

### Main Functionalities
#### CRUD system for entering item information and recording it to a database
    Database Schema:
        transaction information
            int TransactionID (PRIMARY KEY) (required)
            int ItemID (FOREIGN KEY) (required)
            string PurchaseProof (optional) (user uploaded photo, stores link to Azure-stored URL)
        item information
            int ItemID (PRIMARY KEY) (required)
            date PurchaseDate (optional)
            string RetailerVendor (optional)
            string ManufacturerBrand (optional)
            string Color (optional)
            string ItemVersion (optional)
            string ItemDescription (optional)
            string LocationRoom (optional)

#### place for items to be viewed, filtered, sorted, edited, and deleted, ideally all in the same place
#### way to export saved information
    to .csv
    to google sheets

### SECONDARY FUNCTIONALITY
#### wiki 
    resources to learn more about specific fire-insurance related things, tricks, etc.
#### way to automatically scrape this information when payments are made
    browser extension?
    web scraper attached to a URL submission form maybe?
    this may be infeasible
