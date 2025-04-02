* Commands:
 - Start containers và tạo volume: docker-compose up -d
 - Ngưng, xóa tất cả containers và volume: docker-compose down -v
 - Xem log của db: docker-compose logs db
 - Xem log của server: docker-compose logs api

* Test case:
 # Lấy danh sách Id các sản phẩm còn hàng (stock > 0): 
	+ HTTP GET: http://localhost:8080/api/Inventory/GetAvailableProduct

 # Lấy thông tin tồn kho của 1 sản phẩm dựa trên productId: 
	+ HTTP GET: http://localhost:8080/api/Inventory/GetProductQuantity?productId=67e822da6be448e52dfcfa90

 # Tạo order chỉ có 1 sản phẩm:
	+ HTTP POST: http://localhost:8080/api/Order/PlaceOrder

  [
    {
      "UserId": 123,
      "ProductId": "67e822da6be448e52dfcfa8f",
      "Quantity": 2,
      "Price": 50.0
    }
  ]

 # Tạo order có nhiều sản phẩm (quantity của từng sản phẩm vẫn valid) => Tạo Order thành công 
	+ HTTP POST: http://localhost:8080/api/Order/PlaceOrder

  [
    {
      "UserId": 123,
      "ProductId": "67e822da6be448e52dfcfa8f",
      "Quantity": 2,
      "Price": 50.0
    },
    {
      "UserId": 123,
      "ProductId": "67e822da6be448e52dfcfa90",
      "Quantity": 4,
      "Price": 100.0
    }
  ]

 # Tạo order có nhiều sản phẩm (có sản phẩm có quantity > stock) => Cả Order thất bại kể cả sản phẩm có stock > quantity (Đảm bảo data consistency)

  [
    {
      "UserId": 123,
      "ProductId": "67e822da6be448e52dfcfa90", 
      "Quantity": 100, //Đủ quantity, (stock mặc định có sẵn 1000 sp)
      "Price": 100.0
    },
    {
      "UserId": 123,
      "ProductId": "67e822da6be448e52dfcfa8f",
      "Quantity": 10000, //Ko đủ quantity, sản phẩm bên trên đủ quantity cũng ko dc tạo order thành công mà phải roll back, vì cả 2 sản phẩm đều thuộc 1 order (giống 2 phase commit). FE có thể validate khúc này rồi, ko cho tạo order có quantity > stock. Ở đây BE vẫn check lại đề phòng bad case (mùa flash sale, stock giảm cực nhanh trong thời gian ngắn, FE ko thể cứ 0.5s/lần fetch data stock để validate user input)
      "Price": 50.0
    }
  ]
 

