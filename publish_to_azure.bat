docker build -t eses5.azurecr.io/productmanagementsystem -f ./ProductManagementSystem/pms_api/Dockerfile .
docker push eses5.azurecr.io/productmanagementsystem
docker build -t eses5.azurecr.io/inventorymanagementsystem -f ./InventoryManagementSystem/ims_api/Dockerfile .
docker push eses5.azurecr.io/inventorymanagementsystem
docker build -t eses5.azurecr.io/ordermanagementsystem -f ./OrderManagementSystem/oms_api/Dockerfile .
docker push eses5.azurecr.io/ordermanagementsystem
docker build -t eses5.azurecr.io/authservice -f ./AuthenticationService/auth_service/Dockerfile .
docker push eses5.azurecr.io/authservice
docker build -t eses5.azurecr.io/apigateway -f ./Gateway/api_gateway/Dockerfile .
docker push eses5.azurecr.io/apigateway