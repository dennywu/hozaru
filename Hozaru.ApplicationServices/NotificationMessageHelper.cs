using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Hozaru.ApplicationServices
{
    public class NotificationMessageHelper
    {
        public string GenerateDraftMessage(Order order)
        {
            string orderUrl = string.Format("http://localhost:3000/order/{0}", order.Id);
            string items = "";
            foreach (var item in order.Items)
            {
                var row = string.Format("*{0}* x {1}\n", item.Quantity, item.Product.Name);
                items += row;
            }

            string template = @"Halo kak *{0}*. Terimakasih sudah belanja di *MumuBeautyHouse*.

Nomor Orderan kak {0} adalah *{1}*.

Rinciannya:
{2}
SubTotal: Rp {3}
Ongkos Kirim: Rp {4}
Total yang Harus Dibayar: *Rp {5}*


Metode Pembayaran: {6}
Silahkan transfer ke: 
*{7}*
*{8} a/n {9}*

klik link dibawah ini untuk upload bukti pembayaran:
{10}


_Abaikan pesan ini jika kak {0} Sudah Bayar_.";

            var message = string.Format(template, order.CustomerName, 
                order.OrderNumber, 
                items,
                String.Format("{0:#,##0}", order.Summary.SubTotal),
                String.Format("{0:#,##0}", order.Summary.ShippingCost),
                String.Format("{0:#,##0}", order.Summary.Total), 
                order.PaymentType.Name, 
                order.PaymentType.BankName, 
                order.PaymentType.AccountNumber, 
                order.PaymentType.AccountName, 
                orderUrl);
            return WebUtility.UrlEncode(message);
        }

        public string GenerateConfirmationMessage(Order order)
        {
            string orderUrl = string.Format("http://localhost:3000/order/{0}", order.Id);
            var template = @"Halo kak {0}.

Terimakasih atas pembayaran Anda sebesar Rp {1} pada tanggal {2} untuk Orderan {3}.

Kami akan kirim orderan Anda setelah Orderan Anda terverifikasi.

klik link dibawah ini untuk lihat orderan kak {0}:
{4}";
            var message = string.Format(template, 
                order.CustomerName,
                String.Format("{0:#,##0}", order.Summary.Total),
                order.GetLastPayment().PaymentDate.ToString("dd-MMM-yyyy HH:mm:ss"), 
                order.OrderNumber,
                orderUrl);
            return WebUtility.UrlEncode(message);
        }
    }
}
