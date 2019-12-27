export const StatusOrder = {
    ALL: "ALL",
    DRAFT: "DRAFT",
    WAITINGFORPAYMENT: "WAITINGFORPAYMENT",
    PAYMENTREJECTED: "PAYMENTREJECTED",
    PACKAGING: "PACKAGING",
    SHIPPING: "SHIPPING",
    DONE: "DONE",
    VOID: "VOID",
    RETURNED: "RETURNED"
};

const StatusData =
    [{ code: "DRAFT", name: "Belum Bayar" },
    { code: "WAITINGFORPAYMENT", name: "Verifikasi Pembayaran" },
    { code: "PAYMENTREJECTED", name: "Gagal Bayar" },
    { code: "PACKAGING", name: "Perlu Dikirim" },
    { code: "SHIPPING", name: "Sedang Dikirim" },
    { code: "DONE", name: "Selesai" },
    { code: "VOID", name: "Dibatalkan" },
    { code: "RETURNED", name: "Dikembalikan" }];

export const getStatusIndonesia = function (statusOrder) {
    return StatusData.find(i => i.code === statusOrder).name;
};
