using Hozaru.Core.Configurations;
using Hozaru.Domain;
using Hozaru.Identity.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Hozaru.ApplicationServices
{
    public static class NotificationMessageHelper
    {
        public static string GenerateDraftMessage(Order order, Tenant tenant)
        {
            string orderUrl = generateOrderUrl(order, tenant);

            string items = "";
            foreach (var item in order.Items)
            {
                var row = string.Format("*{0}* x {1}\n", item.Quantity, item.Product.Name);
                items += row;
            }

            string template = @"Halo kak *{0}*. Terimakasih sudah belanja di *{11}*.

Nomor Orderan kak {0} adalah *{1}*.

Rinciannya:
{2}
SubTotal: Rp {3}
Ongkos Kirim: Rp {4}
Total yang Harus Dibayar: *Rp {5}*


Metode Pembayaran: {6}
Silahkan transfer ke: 
*Bank {7}*
*{8} a/n {9}*

klik link dibawah ini untuk upload bukti pembayaran:
{10}


_Abaikan pesan ini jika kak {0} Sudah Bayar_.";

            var message = string.Format(template, order.Customer.CustomerName,
                order.OrderNumber,
                items,
                String.Format("{0:#,##0}", order.Summary.SubTotal),
                String.Format("{0:#,##0}", order.Shipment.ShippingCost),
                String.Format("{0:#,##0}", order.Summary.NetTotal),
                order.Payment.PaymentMethod.Bank.Name,
                order.Payment.PaymentMethod.Bank.BankName,
                order.Payment.PaymentMethod.AccountNumber,
                order.Payment.PaymentMethod.AccountName,
                orderUrl,
                tenant.Name);
            return message;
        }

        public static string GenerateConfirmationMessage(Order order, Tenant tenant)
        {
            string orderUrl = generateOrderUrl(order, tenant);

            var template = @"Halo kak *{0}*.

Terimakasih atas pembayaran Anda sebesar *Rp {1}* pada tanggal *{2}* untuk Orderan *{3}*.

Kami akan kirim orderan Anda setelah Orderan Anda terverifikasi.

klik link dibawah ini untuk lihat orderan kak {0}:

{4}";
            var message = string.Format(template,
                order.Customer.CustomerName,
                String.Format("{0:#,##0}", order.Summary.NetTotal),
                order.Payment.GetLastPayment().PaymentDate.ToString("dd MMM yyyy HH:mm:ss"),
                order.OrderNumber,
                orderUrl);
            return message;
        }

        public static string GenerateApprovedMessage(Order order, Tenant tenant)
        {
            string orderUrl = generateOrderUrl(order, tenant);

            var template = @"Halo kak *{0}*.

Terimakasih atas pembayaran Anda sebesar *Rp {1}* pada tanggal *{2}* untuk Orderan *{3}*.

Kami sudah menerima pembayaran Anda.

Pesanan kak *{0}* akan segera kami kemas, dan kirim ke alamat:
{4}

klik link dibawah ini untuk Lacak orderan kak {0}:

{5}";
            var message = string.Format(template,
                order.Customer.CustomerName,
                String.Format("{0:#,##0}", order.Summary.NetTotal),
                order.Payment.GetLastPayment().PaymentDate.ToString("dd MMM yyyy HH:mm:ss"),
                order.OrderNumber,
                order.Customer.GetCustomerFullAddress(),
                orderUrl);
            return message;
        }

        public static string GenerateRejectPaymentMessage(Order order, string reason, Tenant tenant)
        {
            string orderUrl = generateOrderUrl(order, tenant);

            var template = @"Halo kak *{0}*.

Terimakasih atas Konfirmasi Pembayaran Anda pada tanggal *{1}* untuk Orderan *{2}*.

Tapi sayangnya kami tidak bisa setujui Pembayaran tersebut.
{3}

Silahkan lakukan pembayaran dan upload bukti pembayaran kak {0} di:

{4}";
            var message = string.Format(template,
                order.Customer.CustomerName,
                order.Payment.GetLastPayment().PaymentDate.ToString("dd MMM yyyy HH:mm:ss"),
                order.OrderNumber,
                reason,
                orderUrl);
            return message;
        }

        public static string GenerateCancelOrderMessage(Order order, string reason, Tenant tenant)
        {
            string orderUrl = generateOrderUrl(order, tenant);
            var template = @"Halo kak *{0}*.

Sayang sekali, pesanan kak {0} dengan nomor *{1}* sudah dibatalkan.
Ini rincian dari pesanannya:
{2}

Terimaka kasih sudah mempercayai *{3}*.

klik link dibawah ini untuk liat orderan kak {0} yang sudah dibatalkan:

{4}";

            string items = "";
            foreach (var item in order.Items)
            {
                var row = string.Format("*{0}* x {1}\n", item.Quantity, item.Product.Name);
                items += row;
            }

            var message = string.Format(template,
                order.Customer.CustomerName,
                order.OrderNumber,
                items,
                tenant.Name,
                orderUrl);
            return message;
        }

        public static string GenerateCompleteMessage(Order order, Tenant tenant)
        {
            string orderUrl = generateOrderUrl(order, tenant);
            var template = @"Halo kak *{0}*.

Pesanan kak {0} dengan nomor *{1}* sudah sampai dan diterima oleh *{2}* pada *{3}*.

Terimaka kasih sudah belanja di *{4}*.

klik link dibawah ini untuk liat orderan kak {0}:

{5}";

            var message = string.Format(template,
                order.Customer.CustomerName,
                order.OrderNumber,
                order.Shipment.ProofOfDeliveryReceiver.IsNullOrWhiteSpace() ? "yang bersangkutan" : order.Shipment.ProofOfDeliveryReceiver,
                order.Shipment.ProofOfDeliveryDate.HasValue ? order.Shipment.ProofOfDeliveryDate.Value.ToString("dd MMM yyyy HH:mm") : "saat ini",
                tenant.Name,
                orderUrl);
            return message;
        }

        private static string generateOrderUrl(Order order, Tenant tenant)
        {
            var httpProtocol = AppSettingConfigurationHelper.GetSection("MultiTenancyHttpProtocol").Value;
            var domainName = AppSettingConfigurationHelper.GetSection("MultiTenancyDomainName").Value;
            string orderUrl = string.Format("{0}://{1}.{2}/order/{3}", httpProtocol, tenant.TenancyName, domainName, order.Id);

            if (!tenant.ExternalDomain.IsNullOrWhiteSpace())
            {
                orderUrl = string.Format("{0}://{1}/order/{2}", "http", tenant.ExternalDomain, order.Id);
            }

            return orderUrl;
        }
    }
}
