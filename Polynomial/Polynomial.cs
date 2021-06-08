using System;
using System.Collections.Generic;
using PolynomialObject.Exceptions;

namespace PolynomialObject
{
    public sealed class Polynomial 
    {
        List<PolynomialMember> polynomialMembers { get; set; }
      

        public Polynomial()
        {
            polynomialMembers = new List<PolynomialMember>();
        }

        public Polynomial(PolynomialMember member)
        {
            polynomialMembers = new List<PolynomialMember> {
                new PolynomialMember(member.Degree,member.Coefficient)
            };
        } 
        public Polynomial(IEnumerable<PolynomialMember> members)
        {
            polynomialMembers = new List<PolynomialMember>(members);
        }

        public Polynomial((double degree, double coefficient) member)
        {
            polynomialMembers = new List<PolynomialMember>
           {
               new PolynomialMember(member.degree,member.coefficient)
           };
        }

        public Polynomial(IEnumerable<(double degree, double coefficient)> members)
        {
            polynomialMembers.AddRange((IEnumerable<PolynomialMember>)members);
        }

        public int Count
        {
            get
            {
                int count = 0;
                foreach (var x in polynomialMembers)
                {
                    if (x != null)
                        count++;
                }
                return count;
            }
        }

        public double Degree
        {
            get
            {
                double max = 0;
                for (int i = 0; i < polynomialMembers.Count; i++)
                {
                    if (polynomialMembers[i] != null && max < polynomialMembers[i].Degree)
                    {
                        max = polynomialMembers[i].Degree;
                    }
                }
                return max;
            }
        }

        public void AddMember(PolynomialMember member)
        {
            if (member == null)
                throw new PolynomialArgumentNullException();

            if (this.ContainsMember(member.Degree))
                throw new PolynomialArgumentException();

            if (member.Coefficient == 0)
                throw new PolynomialArgumentException();

            if (member.Coefficient != 0)
                this.polynomialMembers.Add(member);
        }

        public void AddMember((double degree, double coefficient) member)
        {
            if (this.ContainsMember(member.degree))
            {
                throw new PolynomialArgumentException();
            }

            if (member.coefficient == 0)
                throw new PolynomialArgumentException();

            PolynomialMember monomial = new PolynomialMember(member.degree, member.coefficient);
                polynomialMembers.Add(monomial);
        }

        public bool RemoveMember(double degree)
        {
            for (int i = 0; i < polynomialMembers.Count;i++)
            {
                if (polynomialMembers[i].Degree == degree)
                {
                    this.polynomialMembers.RemoveAt(i);
                    return true;
                }
            }
           return false;
        }

        public bool ContainsMember(double degree)
        {
           foreach(var x in polynomialMembers.ToArray()) { 
                if (x.Degree == degree)
                    return true;
            }
          return false;
        }

        public PolynomialMember Find(double degree)
        {
            for (int i = 0; i < polynomialMembers.Count; i++)
            {
                if (polynomialMembers[i].Degree == degree)
                    return polynomialMembers[i];
            }
           return null;
        }

        public double this[double degree]
        {

            get
            {
                foreach (PolynomialMember x in polynomialMembers)
                {
                    if (x.Degree == degree)
                    {
                        return x.Coefficient;
                    }

                }
                return 0;
            }
            set
            {
                PolynomialMember member = new PolynomialMember(degree, value);

                foreach (PolynomialMember x in polynomialMembers)
                {
                    if (x.Degree == degree && value != 0)
                        x.Coefficient = value;
                }

                for (int i = 0; i < polynomialMembers.Count; i++)
                {
                    if (polynomialMembers[i].Degree == degree && value == 0)
                    {
                        polynomialMembers.Remove(polynomialMembers[i]);
                    }
                }

                for (int i = 0; i < polynomialMembers.Count; i++)
                {
                    if (polynomialMembers[i].Degree != degree && value != 0)
                    {
                          polynomialMembers.Add(member);
                          return;
                    }
                }
            }
        }

        public PolynomialMember[] ToArray()
        {
           return polynomialMembers.ToArray();
        }

        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            if (a == null || b == null)
                throw new PolynomialArgumentNullException();

            return a.Add(b);
        }

        public static Polynomial operator -(Polynomial a, Polynomial b)
        {
            if (a == null || b == null)
                throw new PolynomialArgumentNullException();

            return a.Subtraction(b);
        }

        public static Polynomial operator *(Polynomial a, Polynomial b)
        {
            if (a == null || b == null)
                throw new PolynomialArgumentNullException();

            return a.Multiply(b);
        }

        public Polynomial Add(Polynomial polynomial)
        {
            if (polynomial == null)
                throw new PolynomialArgumentNullException();

            for (int i = 0; i < polynomialMembers.Count; i++)
            {
                if (polynomial.ContainsMember(polynomialMembers[i].Degree))
                {
                    polynomial.Find(polynomialMembers[i].Degree).Coefficient += polynomialMembers[i].Coefficient;
                    polynomialMembers[i].Coefficient = 0;
                }

                if (polynomialMembers[i].Coefficient == 0)
                    this.RemoveMember(polynomialMembers[i].Coefficient);

                if (polynomial.polynomialMembers[i].Coefficient == 0 || polynomial.polynomialMembers[i].Coefficient == 2)
                    polynomial.polynomialMembers.RemoveAt(i);
            }

            for (int i = 0; i < polynomialMembers.Count; i++)
            { 
                if (polynomialMembers[i].Coefficient != 0 )
                    polynomial.AddMember(polynomialMembers[i]);
            }
            return polynomial;
        }

        public Polynomial Subtraction(Polynomial polynomial)
        {
            if (polynomial == null)
                throw new PolynomialArgumentNullException();

            foreach(var x in polynomial.polynomialMembers.ToArray())
            {
                if (this.ContainsMember(x.Degree) && x.Coefficient != 0)
                {
                        this.Find(x.Degree).Coefficient -= x.Coefficient;
                        x.Coefficient = 0;
                }
                if (!this.ContainsMember(x.Degree) && x.Coefficient != 0)
                {
                        x.Coefficient = -x.Coefficient;
                        this.AddMember(x);
                }
            }

            foreach(var x in polynomialMembers.ToArray())
            {
                if (x.Coefficient == 0)
                    this.RemoveMember(x.Degree);
            }
            return this;
        }

        public Polynomial Multiply(Polynomial polynomial)
        {
            if (polynomial == null)
                throw new PolynomialArgumentNullException();

            Polynomial result = new Polynomial();
            PolynomialMember member;

            for (int i = 0; i < polynomialMembers.Count; i++)
            {
                for (int j = 0; j < polynomial.polynomialMembers.Count; j++)
                {
                    member = (PolynomialMember)this.polynomialMembers[i].Clone();
                    member.Coefficient = polynomialMembers[i].Coefficient * polynomial.polynomialMembers[j].Coefficient;
                    member.Degree = polynomialMembers[i].Degree + polynomial.polynomialMembers[j].Degree;
                    if (!result.ContainsMember(member.Degree))
                        result.polynomialMembers.Add(member);

                    else result.Find(member.Degree).Coefficient += member.Coefficient;
                }
            }

            foreach (var x in result.polynomialMembers.ToArray()) {
                if (x.Coefficient == 0)
                    result.RemoveMember(x.Degree);
            }
                    return result;
        }

        public static Polynomial operator +(Polynomial a, (double degree, double coefficient) b)
        {

            for (int i = 0; i < a.polynomialMembers.Count; i++)
            {
                if (a.polynomialMembers[i].Degree == b.degree)
                {
                    a.polynomialMembers[i].Coefficient += b.coefficient;
                    b.coefficient = 0;
                }
            }
            if (b.coefficient != 0)
                a.AddMember(b);

            foreach(var x in a.polynomialMembers.ToArray())
            {
                if (x.Coefficient == 0)
                    a.RemoveMember(x.Degree);
            }
            return a;
        }

        public static Polynomial operator -(Polynomial a, (double degree, double coefficient) b)
        {
            for (int i = 0; i < a.polynomialMembers.Count; i++)
            {
                if (a.polynomialMembers[i].Degree == b.degree)
                {
                    a.polynomialMembers[i].Coefficient -= b.coefficient;
                    b.coefficient = 0;
                }
            }
            if (b.coefficient != 0)
            {
                b.coefficient = -b.coefficient;
                a.AddMember(b);
            }
            foreach (var x in a.polynomialMembers.ToArray())
            {
                if (x.Coefficient == 0)
                    a.RemoveMember(x.Degree);
            }
            return a;
        }

        public static Polynomial operator *(Polynomial a, (double degree, double coefficient) b)
        {
            Polynomial result = new Polynomial();
            PolynomialMember member;

            foreach (var x in a.polynomialMembers)
            {
                member = (PolynomialMember)x.Clone();
                member.Coefficient = x.Coefficient * b.coefficient;
                member.Degree = x.Degree + b.degree;
                    if (!result.ContainsMember(member.Degree))
                    result.polynomialMembers.Add(member);

                else result.Find(member.Degree).Coefficient += member.Coefficient;
            }

            foreach (var x in result.polynomialMembers.ToArray())
            {
                if (x.Coefficient == 0)
                    result.RemoveMember(x.Degree);
            }
            return result;
        }

        public Polynomial Add((double degree, double coefficient) member)
        {
           return this + member;
        }

        public Polynomial Subtraction((double degree, double coefficient) member)
        {
            return this - member;
        }

 
        public Polynomial Multiply((double degree, double coefficient) member)
        {
            return this * member;
        }
    }
}
