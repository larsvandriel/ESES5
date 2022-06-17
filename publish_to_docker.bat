docker build -t larsvandriel/productmanagementsystem -f ./ProductManagementSystem/pms_api/Dockerfile .
docker push larsvandriel/productmanagementsystem
docker build -t larsvandriel/inventorymanagementsystem -f ./InventoryManagementSystem/ims_api/Dockerfile .
docker push larsvandriel/inventorymanagementsystem
docker build -t larsvandriel/ordermanagementsystem -f ./OrderManagementSystem/oms_api/Dockerfile .
docker push larsvandriel/ordermanagementsystem
docker build -t larsvandriel/authservice -f ./AuthenticationService/auth_service/Dockerfile .
docker push larsvandriel/authservice
docker build -t larsvandriel/apigateway -f ./Gateway/api_gateway/Dockerfile .
docker push larsvandriel/apigateway