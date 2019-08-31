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