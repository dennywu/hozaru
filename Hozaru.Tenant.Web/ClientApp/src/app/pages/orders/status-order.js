export const StatusOrder = {
    ALL: "ALL",
    DRAFT: "DRAFT",
    WAITINGFORPAYMENT: "WAITINGFORPAYMENT",
    PAYMENTREJECTED: "PAYMENTREJECTED",
    PACKAGING: "PACKAGING",
    SHIPPING: "SHIPPING",
    DONE: "DONE",
    CANCELED: "CANCELED"
};

const StatusData =
    [{ code: "DRAFT", name: "Belum Bayar" },
    { code: "WAITINGFORPAYMENT", name: "Verifikasi Pembayaran" },
    { code: "PAYMENTREJECTED", name: "Gagal Bayar" },
    { code: "PACKAGING", name: "Perlu Dikirim" },
    { code: "SHIPPING", name: "Sedang Dikirim" },
    { code: "DONE", name: "Selesai" },
    { code: "CANCELED", name: "Pembatalan" }];

export const getStatusIndonesia = function (statusOrder) {
    return StatusData.find(i => i.code == statusOrder).name;
};
