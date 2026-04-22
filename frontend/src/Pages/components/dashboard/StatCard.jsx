// components/dashboard/StatCard.jsx
const StatCard = ({ title, value }) => {
  return (
    <div className="card-dark p-3 text-center">
      <h6 className="text-muted">{title}</h6>
      <h4 className="text-emerald">{value}</h4>
    </div>
  );
};

export default StatCard;