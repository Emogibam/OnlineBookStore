//using BusinessLogic.Interfaces;
//using DataAccess.DTOs;
//using DataAccess.Entities;
//using DataAccess.Interfaces;
//using DataAccess.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace BusinessLogic.BusinessLogics
//{
//    public class OrderLogic :IOrderLogic
//    {
//        private readonly IOrderRepository orderRepository;
//        private readonly IBookRepository bookRepository;

//        public OrderLogic(IOrderRepository orderRepository, IBookRepository bookRepository)
//        {
//            this.orderRepository = orderRepository;
//            this.bookRepository = bookRepository;
//        }

//        public void CreateOrder(OrderDTO orderDTO)
//        {
//            var order = MapDTOToOrder(orderDTO);
//            orderRepository.CreateOrder(order);
//            UpdateBookQuantities(orderDTO.OrderItems);
//        }

//        public IEnumerable<OrderDTO> GetOrdersByUserId(Guid userId)
//        {
//            var orders = orderRepository.GetOrdersByUserId(userId);
//            return MapOrdersToDTOs(orders);
//        }

//        public void UpdateOrderStatus(Guid orderId, string status)
//        {
//            orderRepository.UpdateOrderStatus(orderId, status);
//        }

//        private Order MapDTOToOrder(OrderDTO orderDTO)
//        {
//            return new Order
//            {
//                Id = orderDTO.Id,
//                UserId = orderDTO.UserId,
//                OrderDate = orderDTO.OrderDate,
//                TotalAmount = orderDTO.TotalAmount,
//                Status = orderDTO.Status,
//                OrderItems = MapDTOToOrderItems(orderDTO.OrderItems)
//            };
//        }

//        private List<OrderItem> MapDTOToOrderItems(List<OrderItemDTO> orderItemDTOs)
//        {
//            return orderItemDTOs.Select(orderItemDTO => new OrderItem
//            {
//                Id = orderItemDTO.Id,
//                OrderID = orderItemDTO.OrderId,
//                BookID = orderItemDTO.BookId,
//                Quantity = orderItemDTO.Quantity,
//                Subtotal = orderItemDTO.Subtotal
//            }).ToList();
//        }

//        private IEnumerable<OrderDTO> MapOrdersToDTOs(IEnumerable<Order> orders)
//        {
//            return orders.Select(order => new OrderDTO
//            {
//                Id = order.Id,
//                UserId = order.UserId,
//                OrderDate = order.OrderDate,
//                TotalAmount = order.TotalAmount,
//                Status = order.Status,
//                OrderItems = MapOrderItemsToDTOs(order.OrderItems)
//            });
//        }

//        private List<OrderItemDTO> MapOrderItemsToDTOs(List<OrderItem> orderItems)
//        {
//            return orderItems.Select(orderItem => new OrderItemDTO
//            {
//                Id = orderItem.Id,
//                OrderId = orderItem.OrderID,
//                BookId = orderItem.BookID,
//                Quantity = orderItem.Quantity,
//                Subtotal = orderItem.Subtotal
//            }).ToList();
//        }

//        private void UpdateBookQuantities(List<OrderItemDTO> orderItems)
//        {
//            foreach (var orderItem in orderItems)
//            {
//                var book = bookRepository.GetBookById(orderItem.BookId);
//                book.Quantity -= orderItem.Quantity;
//                bookRepository.UpdateBook(book);
//            }
//        }
//    }
//}
