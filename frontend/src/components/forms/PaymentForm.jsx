import { useState } from "react";
import { CardElement, useStripe, useElements } from "@stripe/react-stripe-js";
import { confirmPayment } from "../../services/paymentService";
const PaymentForm = ({ clientSecret, paymentIntentId, onSuccess }) => {
  const stripe = useStripe();
  const elements = useElements();

  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!stripe || !elements) return;

    setLoading(true);

    const cardElement = elements.getElement(CardElement);

    const result = await stripe.confirmCardPayment(clientSecret, {
      payment_method: {
        card: cardElement,
      },
    });

    if (result.error) {
      alert(result.error.message);
      setLoading(false);
      return;
    }

    // 🔥 Confirm on backend
    await confirmPayment({
      paymentIntentId,
    });

    alert("Payment Successful 🎉");

    onSuccess();
    setLoading(false);
  };

  return (
    <form onSubmit={handleSubmit} className="card-dark p-4">

      <h5 className="text-emerald mb-3">Pay Now 💳</h5>

      <div className="mb-3">
        <CardElement />
      </div>

      <button className="btn btn-emerald w-100" disabled={loading}>
        {loading ? "Processing..." : "Pay"}
      </button>

    </form>
  );
};

export default PaymentForm;