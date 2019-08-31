$name = "customer-api-tf"
$rg = "$name-rg"
$storage = "customerapist"

& az group create --location eastus2 --name "$rg"
& az storage account create --name "$storage" --resource "$rg" --location eastus2 --sku Standard_LRS
& az storage container create --name terraform --account-name "$storage"

#$k = & az storage account keys list -g "customer-api-tf-rg" -n "customerapist" --query [0].value
#C:\terraform\terraform.exe init --backend-config="key=$k"
