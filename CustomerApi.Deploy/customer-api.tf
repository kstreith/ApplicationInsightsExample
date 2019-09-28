terraform {
    required_version = ">= 0.12"
    backend "azurerm" {
        storage_account_name = "customerapist"
        container_name = "terraform"
        key = "terraform.tfstate"
    }
}

provider "azurerm" {
    version = "=1.33.0"
}

variable "sql_password" {
    type = "string"
    description = "Azure SQL Server Password"
}
resource "azurerm_resource_group" "dev" {
    name = "customer-api-rg"
    location = "East US2"
}

resource "azurerm_app_service_plan" "dev" {
    name = "customer-api-plan"
    location = "${azurerm_resource_group.dev.location}"
    resource_group_name = "${azurerm_resource_group.dev.name}"

    sku {
        tier = "Free"
        size = "F1"
    }
}

resource "azurerm_app_service" "dev" {
    name = "customer-api-itsnull"
    location = "${azurerm_resource_group.dev.location}"
    resource_group_name = "${azurerm_resource_group.dev.name}"
    app_service_plan_id = "${azurerm_app_service_plan.dev.id}"
}

resource "azurerm_application_insights" "dev" {
	name = "customer-api-appinsights"
	location = "${azurerm_resource_group.dev.location}"
	resource_group_name = "${azurerm_resource_group.dev.name}"
	application_type = "web"
}

output "instrumentation_key" {
	value = "${azurerm_application_insights.dev.instrumentation_key}"
}

resource "azurerm_sql_server" "dev" {
  name                         = "customerapisqldb"
  resource_group_name          = "${azurerm_resource_group.dev.name}"
  location                     = "${azurerm_resource_group.dev.location}"
  version                      = "12.0"
  administrator_login          = "4dm1n157r470r"
  administrator_login_password = "${var.sql_password}"
}


resource "azurerm_sql_database" "dev" {
  name                             = "customerapi"
  resource_group_name              = "${azurerm_resource_group.dev.name}"
  location                         = "${azurerm_resource_group.dev.location}"
  server_name                      = "${azurerm_sql_server.dev.name}"
  edition			               = "Standard"
  requested_service_objective_name = "S0"
}

resource "azurerm_sql_firewall_rule" "test" {
  name                = "AllowAzureAccess"
  resource_group_name = "${azurerm_resource_group.dev.name}"
  server_name         = "${azurerm_sql_server.dev.name}"
  start_ip_address    = "0.0.0.0"
  end_ip_address      = "0.0.0.0"
}

resource "azurerm_cosmosdb_account" "dev" {
  name                = "customerapicdb"
  location            = "${azurerm_resource_group.dev.location}"
  resource_group_name = "${azurerm_resource_group.dev.name}"
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"

  enable_automatic_failover = true

  consistency_policy {
    consistency_level       = "Session"
  }

  geo_location {
    location          = "${azurerm_resource_group.dev.location}"
    failover_priority = 0
  }
}

output "cosmosdb_endpoint" {
	value = "${azurerm_cosmosdb_account.dev.endpoint}"
}

output "cosmosdb_key" {
	value = "${azurerm_cosmosdb_account.dev.primary_master_key}"
}

output "sqldb_domainname" {
	value = "${azurerm_sql_server.dev.fully_qualified_domain_name}"
}