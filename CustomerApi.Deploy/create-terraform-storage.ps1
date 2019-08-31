$name = "customer-api-tf"
$rg = "$name-rg"
$storage = "customerapist"

& az group create --location eastus2 --name "$rg"
& az storage account create --name "$storage" --resource "$rg" --location eastus2 --sku Standard_LRS
& az storage container create --name terraform --account-name "$storage"

$storageKey = & az storage account keys list -g "customer-api-tf-rg" -n "customerapist" --query [0].value
& terraform.exe init --backend-config="access_key=$storageKey"
