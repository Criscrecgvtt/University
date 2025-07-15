(deffacts bfini (puzzle 0 2 3 1 8 4 7 6 5))

(defrule esquerra
  (puzzle $?x ?y 0 $?z)
  (test (<> (length$ $?x) 2))
  (test (<> (length$ $?x) 5)) =>
  (assert (puzzle $?x 0 ?y $?z)))

(defrule dreta
  (puzzle $?x 0 ?y $?z)
  (test (<> (length$ $?x) 2))
  (test (<> (length$ $?x) 5)) =>
  (assert (puzzle $?x ?y 0 $?z)))

(defrule dalt
  (puzzle $?x ?a ?b ?c 0 $?y) =>
  (assert (puzzle $?x 0 ?b ?c ?a $?y)))

(defrule baix
  (puzzle $?x 0 ?a ?b ?c $?z) =>
  (assert (puzzle $?x ?c ?a ?b 0 $?z)))

(defrule objectiu
  (puzzle 1 2 3 8 0 4 7 6 5) =>
  (printout t "Solució trobada!" crlf)
  (halt))