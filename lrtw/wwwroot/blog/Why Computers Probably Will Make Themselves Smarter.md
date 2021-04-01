2021-04-01 #writing #ai #technology

Recently, author Ted Chiang wrote an article entitled [Why Computers Won’t Make Themselves Smarter](https://www.newyorker.com/culture/annals-of-inquiry/why-computers-wont-make-themselves-smarter). In this article, Chiang argues that concerns around a self-iterating Artificial General Intelligence (AGI) emerging as a superintelligence are unfounded.

> We fear and yearn for “the singularity.” But it will probably never come.

I'd like to address some of the ideas in this article from the perspective of someone who probably has more knowledge about AI than the average punter, but who probably isn't going to be winning any prizes anytime soon - so I'll get some stuff wrong. But it does seem like this article represents a layman's understanding of these concepts, and I think drilling down into the nitty gritty a bit more might reveal some flaws.

This is in no way meant to attack or demean Chiang's great piece - it was a well-written expression of common perceptions around this issue. This is precisely why I think it is worthwhile to address. In this spirit, instead of the typical "forensic dissection" tactic often seen when critiquing articles (which can lose sight of the forest in the trees) I will attempt to summarise Chiang's claims and address them one-by-one.

# Claims

As far as I can make out, Chiang makes X problematic claims in order to support the proposition that a self-iterating AGI is not a threat.

## 1. AI researchers measure intelligence through a single number.

There is no real formal definition within artificial intelligence research for "intelligence", and there is definitely no belief that such a property could be meaningfully captured through a single metric. Chiang describes thinking about human intelligence with a single metric as "for the sake of convenience", but then rightly says:

> Do we have any reason to think that this is the way intelligence works? I don’t believe that we do.

Every AI researcher would absolutely agree that this is not how intelligence works. They might describe intelligence simply as the ability to achieve their goals in a complex environment, or the ability to generalize and abstract information around them, or any plethora of ways - but very, very few claim to have reduced it down to one heuristic. I wish!

## 2. There is an analog between human self-iteration and artificial self-iteration

Chiang gives us an anthropomorphized analogy of human self-iteration, where a very smart person attempts to solve the problem of how to make all the equally smart people around them even smarter. This analogy appeals to us because we can imagine it easily, but it does not capture the real circumstances under which an AGI would grow.

> Once there’s a person with an I.Q. of, say, 300, one of the problems this person can solve is how to convert a person with an I.Q. of 300 into a person with an I.Q. of 350.

There is, of course, an argument to magnitude here - why are we iterating in steps of "50 IQ"? I think mostly because the argument sounds a little less convincing if we reduce the iterative step by a lot. Sure, a "300 IQ teacher" might struggle to produce a "350 IQ student" - but is a "301 IQ student" more imaginable to you? If that seems more possible, it's just a matter of iterating 50 times. An iterative process only needs to produce a *slightly better result with repeatability* to produce very large improvements over many iterations.

Chiang talks a little more about the impossible complexity of the connectome, and then extends this to the following:

> Some proponents of an intelligence explosion argue that it’s possible to increase a system’s intelligence without fully understanding how the system works. They imply that intelligent systems, such as the human brain or an A.I. program, have one or more hidden “intelligence knobs,” and that we only need to be smart enough to find the knobs.

> Let’s imagine that we have an A.I. program that is just as intelligent and capable as the average human computer programmer. Now suppose that we increase its computer’s speed a hundred times and let the program run for a year. That’d be the equivalent of locking an average human being in a room for a hundred years, with nothing to do except work on an assigned programming task.

This anthropomorphism of an artificial intelligence is a dangerous game. It leads us to make incorrect conclusions about a thing which is not human, based upon our understanding of how humans work. A major lesson - indeed the [Bitter Lesson](http://incompleteideas.net/IncIdeas/BitterLesson.html) - of the last few decades of research is that all of our fancy human ideas about how to make an AI better cannot compete with just throwing more compute at the problem. While much research is being done into building AIs that can [explain themselves](https://en.wikipedia.org/wiki/Explainable_artificial_intelligence), this idea that we can build very complicated black-boxes that are far beyond our understanding, and yet display intelligent properties, is based in our very real, current day reality. The primary "intelligence knob" we possess is compute, and we do not need to understand the final networks that emerge in order for those networks to display intelligence.

It is here where our analogy to human intelligence truly breaks down. Our intelligence is relatively stable throughout our lifetimes, and you can't just attach another brain to your own and double your compute. That is not the case for artificial intelligence. We are getting extremely good at producing enormous amounts of compute, and an ML model (e.g. GPT) can use up as much as you can give it.

## 3. The dangers of "bootstrapped" AGI

Chiang talks about bootstrapping compilers and this is, in my opinion, actually quite a good analogy. But Chiang somewhat misinterprets the concept of bootstrapping, and its implications for unknowability.  [I've talked a little bit previously about how we can think of this analogy in depth here](http://lrtw.net/blog/isaself-iteratingagivulnerabletothompson-styletrojans) and some of the threats it implies, so I won't repeat myself, but this ties directly into what I say there. The threat described within compilers - Thompson-style trojans - is precisely *because* the semantics of the compiler machine code are not realistically computable.

## 4. AGI will Optimize For Generality

Chiang states that there is some limit to how generalizable an intelligence can be, and that an intelligence explosion requires there to be no limit. But this is, I believe, another case of anthropomorphizing intelligence. It is indeed entirely possible that there is some limit to how general an intelligence can be, *given some amount of finite compute*. But it is also unreasonable to imagine that such a limit should correlate to human biological patterns. The limit could reasonably be a few orders of magnitude above human intelligence. We have a tendency to peg the intelligence level of human beings as some intrinsically special point within the space of intelligence, but it is just an arbitrary level. The universe has no in-built opinion on the subject.

There is also the assumption that an AGI needs to be actively and consciously optimizing for generalizability in order to achieve it, but I'm not sure that follows as well. Generalizability of intelligence may be a metric that an outside observer tracks about an intelligence, and trains that intelligence against, but that is a fundamentally different thing to it being an internal heuristic that we have to understand to improve. This is because, within modern AI strategies at least, *generalizability is a statistical phenomenon, not a logical one*. While I always warn against anthropomorphism, this does at least track with my subjective experience of generalizable intelligence - I don't necessarily have to understand *how* I drew a connection between two different but connected concepts to draw that connection.

# Conclusion

Chiang does continue on talking about societal iteration, and it's some interesting stuff. Thanks for the stimulating read Ted! It was a well written and considered position which I enjoyed thinking about. In summary, I would generally warn against anthropomorphizing artificial intelligence and employing analogies that attempt to imagine an AGI as just "a very smart person".


