(deffacts data (list 4 5 3 46 12 10))

(defrule sort
  ?f <- (list $?x ?y ?z $?w)
  (test (< ?z ?y))
  => 
  (printout t "$?x = " $?x " ?y = " ?y " ?z = " ?z " $?w = " $?w crlf) 
  (retract ?f)
  (assert (list $?x ?z ?y $?w)))